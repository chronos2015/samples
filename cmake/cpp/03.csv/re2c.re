#include<iostream>
#include<vector>

int nextToken();

char* yyin;

std::string yyout;

int nextToken() {
	yyout = "";
start:
/*!re2c 
re2c:define:YYCTYPE  = "char";
re2c:define:YYCURSOR = yyin;
re2c:yyfill:enable = 0;
re2c:indent:top = 1;
'"'     { goto quote; }
','     { return 1; }
'\r\n'  { return 2; }
[\r\n]  { return 2; }
[^\000] { yyout += *(yyin - 1); goto start; }
[\000]  { return 0; }
*/
quote:
/*!re2c
'""'    { yyout += '"'; goto quote; }
'"'     { goto start; }
[^\000] { yyout += *(yyin - 1); goto quote; }
[\000]  { return -1; }
*/
}

int main() {
	yyin = " 123,456 \r\n\"789,012\",\"345\r\n567\"";
	while(true) {
		int i = nextToken();
		std::cout << "@" << yyout << "@" << std::endl;
		if (i == 2) {
			std::cout << "END OF LINE" << std::endl;
		}
		if (i == -1) {
			std::cout << "ERROR" << std::endl;
			return -1;
		}
		if (i == 0) {
			break;
		}
	}
	return 0;
}
