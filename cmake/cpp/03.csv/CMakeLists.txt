cmake_minimum_required(VERSION 3.14)

project(sample)

add_custom_command(OUTPUT re2c.cpp
	COMMAND re2c.exe "${CMAKE_SOURCE_DIR}\\re2c.re" > re2c.cpp
	DEPENDS re2c.re)

add_executable(sample re2c.cpp)
