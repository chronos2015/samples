project(icon)

if(WIN32)
set(APP_ICON ${CMAKE_CURRENT_SOURCE_DIR}/icon.rc)
set(APP_BUNDLE WIN32)
endif()

add_executable(icon ${APP_ICON} icon.cpp)
