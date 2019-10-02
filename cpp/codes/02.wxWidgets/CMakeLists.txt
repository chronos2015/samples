cmake_minimum_required(VERSION 3.14)

project(
	Clipboard
)

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

if(${CMAKE_STATIC_LINKER_FLAGS} STREQUAL "/machine:x64")
	set(wxWidgetLibraryFolder "${WX_WIDGETS_DIR}/lib/vc_x64_lib")
	target_include_directories(
		Clipboard
		PUBLIC
		"${WX_WIDGETS_DIR}/lib/vc_x64_lib/mswu"
	)
else()
	set(wxWidgetLibraryFolder "${WX_WIDGETS_DIR}/lib/vc_lib")

	target_include_directories(
		Clipboard
		PUBLIC
		"${WX_WIDGETS_DIR}/lib/vc_lib/mswu"
	)
endif()
message(${wxWidgetLibraryFolder})

target_link_libraries(
	Clipboard
	PUBLIC
	kernel32 user32 gdi32 comdlg32 winspool winmm shell32 shlwapi comctl32 ole32 oleaut32 uuid rpcrt4 advapi32 version wsock32 wininet odbc32 odbccp32
)

if(${CMAKE_BUILD_TYPE} STREQUAL "Release")
	target_link_libraries(
		Clipboard
		PUBLIC
		"${wxWidgetLibraryFolder}/wxbase31u.lib"
		"${wxWidgetLibraryFolder}/wxbase31u_net.lib"
		"${wxWidgetLibraryFolder}/wxbase31u_xml.lib"
		"${wxWidgetLibraryFolder}/wxexpat.lib"
		"${wxWidgetLibraryFolder}/wxjpeg.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_adv.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_aui.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_core.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_gl.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_html.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_media.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_propgrid.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_qa.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_ribbon.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_richtext.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_stc.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_webview.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_xrc.lib"
		"${wxWidgetLibraryFolder}/wxpng.lib"
		"${wxWidgetLibraryFolder}/wxregexu.lib"
		"${wxWidgetLibraryFolder}/wxscintilla.lib"
		"${wxWidgetLibraryFolder}/wxtiff.lib"
		"${wxWidgetLibraryFolder}/wxzlib.lib")

else()
	target_link_libraries(
		Clipboard
		PUBLIC
		"${wxWidgetLibraryFolder}/wxbase31ud.lib"
		"${wxWidgetLibraryFolder}/wxbase31u_netd.lib"
		"${wxWidgetLibraryFolder}/wxbase31u_xmld.lib"
		"${wxWidgetLibraryFolder}/wxexpatd.lib"
		"${wxWidgetLibraryFolder}/wxjpegd.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_advd.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_auid.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_cored.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_gld.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_htmld.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_mediad.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_propgridd.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_qad.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_ribbond.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_richtextd.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_stcd.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_webviewd.lib"
		"${wxWidgetLibraryFolder}/wxmsw31u_xrcd.lib"
		"${wxWidgetLibraryFolder}/wxpngd.lib"
		"${wxWidgetLibraryFolder}/wxregexud.lib"
		"${wxWidgetLibraryFolder}/wxscintillad.lib"
		"${wxWidgetLibraryFolder}/wxtiffd.lib"
		"${wxWidgetLibraryFolder}/wxzlibd.lib")
endif()
