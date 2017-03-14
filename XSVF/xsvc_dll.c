// XSVF.cpp : Definiert die exportierten Funktionen für die DLL-Anwendung.
//

#include "XSVF.h"

XSVFL_API int LibXSFV_Play(struct libxsvf_host *h, enum libxsvf_mode mode)
{
	return libxsvf_play(h, mode);
}

XSVFL_API const char *LibXSFV_State2str(enum libxsvf_tap_state tap_state)
{
	return libxsvf_state2str(tap_state);
}

XSVFL_API const char *LibXSFV_Mem2str(enum libxsvf_mem which)
{
	return libxsvf_mem2str(which);
}
