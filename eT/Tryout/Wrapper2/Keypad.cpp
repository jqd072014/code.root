// Keypad.cpp : Implementation of CKeypad

#include "stdafx.h"
#include "Keypad.h"

HMODULE	hMWXUSB = LoadLibrary(L"MWXUSB.DLL");

typedef unsigned char (OPEN_USB)(void);
OPEN_USB *lpopenusb = (OPEN_USB*)GetProcAddress(hMWXUSB, "Open_USB");

typedef unsigned char (ACCEPT_LED)(int data);
ACCEPT_LED *lpacceptled = (ACCEPT_LED*)GetProcAddress(hMWXUSB, "Accept_LED");


typedef void (SET_CALLBACK)(void(__stdcall*lpfn)(int));
SET_CALLBACK *lpcallback = (SET_CALLBACK*)GetProcAddress(hMWXUSB, "Set_Callback");


static CKeypad* kp;
void __stdcall CB(int value)
{
	kp->Fire_Callback(value);
}



// CKeypad

STDMETHODIMP CKeypad::Open_USB()
{
	unsigned char ucOpen = (*lpopenusb)();
	kp = this;
	(*lpcallback)(CB);
	return S_OK;
}
