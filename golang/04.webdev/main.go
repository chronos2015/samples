package main

import (
	"fmt"
	"os"

	"github.com/studio-b12/gowebdav"
)

func main() {
	fmt.Println("start")

	client := gowebdav.NewClient("接続URL", "ログインID", "パスワード")
	err := client.Connect()
	if err != nil {
		panic(err)
	}

	// ファイル一覧
	// /から始まる絶対パスを指定すべきらしい
	list, err := client.ReadDir("/")
	if err != nil {
		panic(err)
	}
	for i, file := range list {
		fmt.Printf("%02d,%s\n", i, file.Name())
	}

	// フォルダ作成
	// ※存在するフォルダを再度作成してもエラーにならない.
	// ※ファイルが存在するフォルダを再作成しても問題なし
	err = client.Mkdir("cyndi", 0755)
	if err != nil {
		panic(err)
	}

	// ファイル書き込み
	// ※存在しないフォルダに書き込むとエラーになる.
	err = client.Write("/first.txt", []byte("おはよう"), 0755)
	if err != nil {
		panic(err)
	}

	// ファイル読み込み
	// ファイルが存在しない場合に対応
	data, err := client.Read("/first.txt")
	if err != nil {
		if _, ok := err.(*os.PathError); ok {
			data = []byte{}
		} else {
			panic(err)
		}
	}
	fmt.Printf("%s\n", data)

	// ファイル名変更
	err = client.Rename("/first.txt", "/secound.txt", false)
	if err != nil {
		panic(err)
	}

	fmt.Println("end")
}

// B3mufJTE
