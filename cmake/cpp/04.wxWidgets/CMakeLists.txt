cmake_minimum_required(VERSION 3.14)

project(
	Clipboard
)

# D:\3_thiredpaty\wxWidgets\wxWidgets-3.1.2\include

add_executable(
	Clipboard
	WIN32
	"clipboard.cpp"
)

target_include_directories(
	Clipboard
	PUBLIC
	"${WX_WIDGETS_DIR}/include"
)

message(${CMAKE_SYSTEM_PROCESSOR})
if(${CMAKE_STATIC_LINKER_FLAGS} STREQUAL "/machine:x64")
	file(
		GLOB_RECURSE wxWidgetLibrary
		"${WX_WIDGETS_DIR}/lib/vc_x64_lib/*.lib")
	target_include_directories(
		Clipboard
		PUBLIC
		"${WX_WIDGETS_DIR}/lib/vc_x64_lib/mswu"
	)
else()
	file(
		GLOB_RECURSE wxWidgetLibrary
		"${WX_WIDGETS_DIR}/lib/vc_lib/*.lib")
	target_include_directories(
		Clipboard
		PUBLIC
		"${WX_WIDGETS_DIR}/lib/vc_lib/mswu"
	)
endif()

target_link_libraries(
	Clipboard
	PUBLIC
	${wxWidgetLibrary})

target_link_libraries(
	Clipboard
	PUBLIC
	kernel32 user32 gdi32 comdlg32 winspool winmm shell32 shlwapi comctl32 ole32 oleaut32 uuid rpcrt4 advapi32 version wsock32 wininet odbc32 odbccp32
)
