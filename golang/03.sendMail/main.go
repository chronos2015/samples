package main

import (
	"bytes"
	"encoding/base64"
	"fmt"
	"mime/multipart"
	"mime/quotedprintable"
	"net/smtp"
	"net/textproto"
	"sort"
	"strings"
)

func main() {
	smtpInfo := SMTPInfo{
		Server:   "smtp.example.com",
		Port:     22,
		Login:    "smtpLoginId",
		Password: "smtpLoginPassword",
	}
	msg := MailMessage{
		From: InternetAddress{Address: MailAddress("test@localhost"), Personal: "テスト"},
		To: []InternetAddress{
			{Address: MailAddress("test@localhost"), Personal: "テスト"}},
		Subject:  "本日の天気について",
		TextBody: "本日は晴天なり、本日は晴天なり、本日は晴天なり。",
		HTMLBody: "<html><body>本日は晴天なり、本日は晴天なり、本日は晴天なり。</body></html>",
		AttachdFile: []MailFile{
			{Name: "sample.txt", ContentType: "text/plain", Data: []byte("本日は晴天なり、本日は晴天なり、本日は晴天なり。")},
		},
	}

	err := SendMail(smtpInfo, msg)
	if err != nil {
		panic(err)
	}
}

// SendMail メールを送信
func SendMail(si SMTPInfo, msg MailMessage) error {
	s := fmt.Sprintf("%s:%d", si.Server, si.Port)
	f, t, b, err := createMessage(msg)
	if err != nil {
		return err
	}

	a := smtp.PlainAuth("", si.Login, si.Password, si.Server)
	err = smtp.SendMail(s, a, f, t, b)
	if err != nil {
		return err
	}
	return nil
}

func createMessage(msg MailMessage) (f string, t []string, b []byte, err error) {
	h, b, err := createMessageSub(msg)
	if err != nil {
		return "", nil, nil, err
	}

	h.Add("From", msg.From.format())

	if msg.ReplayTo != nil {
		h.Add("Reply-To", msg.ReplayTo.format())
	}

	var s []string
	for _, a := range msg.To {
		t = append(t, string(a.Address))
		s = append(s, a.format())
	}

	h.Add("To", strings.Join(s[:], ","))

	if len(msg.Cc) > 0 {
		var ccAddr []string
		for _, a := range msg.Cc {
			t = append(t, string(a.Address))
			ccAddr = append(ccAddr, a.format())
		}

		h.Add("Cc", strings.Join(s[:], ","))
	}

	for _, a := range msg.Bcc {
		t = append(t, string(a))
	}

	var bf bytes.Buffer
	keys := make([]string, 0, len(h))
	for k := range h {
		keys = append(keys, k)
	}

	sort.Strings(keys)
	for _, k := range keys {
		for _, v := range h[k] {
			_, err = fmt.Fprintf(&bf, "%s: %s\r\n", k, v)
			if err != nil {
				return "", nil, nil, err
			}
		}
	}

	_, err = fmt.Fprintf(&bf, "\r\n")
	if err != nil {
		return "", nil, nil, err
	}
	bf.Write(b)

	return string(msg.From.Address), t, bf.Bytes(), nil
}

func (i InternetAddress) format() string {
	return fmt.Sprintf("=?UTF-8?B?%s?= <%s>", base64Encode([]byte(i.Personal)), i.Address)
}

func createMessageSub(msg MailMessage) (h textproto.MIMEHeader, b []byte, err error) {
	h, b, err = createBody(msg)
	if err != nil {
		return h, b, err
	}

	if len(msg.AttachdFile) == 0 {
		return h, b, nil
	}

	h1 := make(textproto.MIMEHeader)
	h1.Add("Content-Type", "multipart/mixed")
	var bf bytes.Buffer
	mpWriter := multipart.NewWriter(&bf)
	writer, err := mpWriter.CreatePart(h)
	if err != nil {
		return h, b, err
	}

	_, err = writer.Write(b)
	if err != nil {
		return h, b, err
	}

	for _, a := range msg.AttachdFile {
		h2 := make(textproto.MIMEHeader)
		h2.Add("Content-Type", fmt.Sprintf("%s; name=%s", a.ContentType, a.Name))
		d, e, err := encode(a.Data)
		if err != nil {
			return h, b, err
		}
		h2.Add("Content-Transfer-Encoding", e)
		writer, err := mpWriter.CreatePart(h2)
		if err != nil {
			return h, b, err
		}

		_, err = writer.Write([]byte(d))
		if err != nil {
			return h, b, err
		}
	}

	err = mpWriter.Close()
	if err != nil {
		return h, b, err
	}

	return h1, bf.Bytes(), nil
}

func createBody(msg MailMessage) (h textproto.MIMEHeader, b []byte, err error) {
	var h1 textproto.MIMEHeader
	var b1 []byte
	c1 := true
	if msg.TextBody != "" || msg.HTMLBody == "" {
		h1, b1, err = createBodyPart("text/plain", msg.TextBody)
		if err != nil {
			return nil, b, err
		}
		c1 = false
	}

	var h2 textproto.MIMEHeader
	var b2 []byte
	c2 := true
	if msg.HTMLBody != "" {
		h2, b2, err = createBodyPart("text/html", msg.HTMLBody)
		if err != nil {
			return nil, b, err
		}
		c2 = false
	}

	if c2 {
		return h1, b1, nil
	}

	if c1 {
		return h2, b2, nil
	}

	h = make(textproto.MIMEHeader)
	h.Add("Content-Type", "multipart/alternative")
	var bf bytes.Buffer
	mpWriter := multipart.NewWriter(&bf)
	writer, err := mpWriter.CreatePart(h1)
	if err != nil {
		return nil, b, err
	}

	_, err = writer.Write(b1)
	if err != nil {
		return nil, b, err
	}
	writer, err = mpWriter.CreatePart(h2)
	if err != nil {
		return nil, b, err
	}

	_, err = writer.Write(b2)
	err = mpWriter.Close()
	return h, bf.Bytes(), nil
}

func createBodyPart(cType string, data string) (textproto.MIMEHeader, []byte, error) {
	h := make(textproto.MIMEHeader)
	h.Add("Content-Type", cType)
	d, e, err := encode([]byte(data))
	if err != nil {
		return nil, nil, err
	}
	h.Add("Content-Transfer-Encoding", e)
	return h, []byte(d), nil
}

// qeエンコードとbase64エンコードを実施・比較し短いほうを返却
func encode(data []byte) (string, string, error) {
	ba := base64Encode(data)
	qe, err := qeEncode(data)
	if err != nil {
		return "", "", err
	}
	if len(ba) < len(qe) {
		return ba, "base64", nil
	}
	return qe, "quoted-printable", nil
}

// base64エンコード
func base64Encode(data []byte) string {
	return base64.StdEncoding.EncodeToString(data)
}

// qeエンコード
func qeEncode(data []byte) (string, error) {
	b := bytes.NewBuffer(make([]byte, 0, 1024))
	w := quotedprintable.NewWriter(b)
	_, err := w.Write(data)
	if err != nil {
		return "", err
	}
	err = w.Close()
	if err != nil {
		return "", err
	}
	return b.String(), nil
}

// SMTPInfo SMTPサーバと認証情報
type SMTPInfo struct {
	// Server サーバーのアドレス
	Server string
	// Port ポート番号
	Port uint16
	// Login ログイン名
	Login string
	// Password パスワード
	Password string
}

// MailMessage メール
type MailMessage struct {
	// From 送信元
	From InternetAddress
	// ReplayTo 返信先
	ReplayTo *InternetAddress
	// To 送信先
	To []InternetAddress
	// Cc CC送信先
	Cc []InternetAddress
	// Bcc BCC送信先
	Bcc []MailAddress
	// Subject 件名
	Subject string
	// TextBody テキスト形式の本文
	TextBody string
	// HTMLBody Html形式の本文
	HTMLBody string
	// AttachdFile 添付ファイル
	AttachdFile []MailFile
}

// MailFile 添付ファイル
type MailFile struct {
	// Name ファイル名を表す
	Name string
	// ContentType ファイルの形式を表す
	ContentType string
	// Data データを表す
	Data []byte
}

// InternetAddress 送信先・受信元のメールアドレスを表す
type InternetAddress struct {
	// Address メールアドレス
	Address MailAddress
	// Personal 表示名称
	Personal string
}

// MailAddress メールアドレス
type MailAddress string
