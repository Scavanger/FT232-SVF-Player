#pragma once

#define WIN32_LEAN_AND_MEAN             // Selten verwendete Komponenten aus Windows-Headern ausschlieﬂen

#include <stdlib.h>
#include <windows.h>

#include "libxsvf.h"

#ifdef XSVF_EXPORTS
#define XSVFL_API __declspec(dllexport)
#else
#define XSVFL_API __declspec(dllimport)
#endif

XSVFL_API int LibXSFV_Play(struct libxsvf_host *, enum libxsvf_mode mode);
XSVFL_API const char *LibXSFV_State2str(enum libxsvf_tap_state tap_state);
XSVFL_API const char *LibXSFV_Mem2str(enum libxsvf_mem which);






