// Keypad.cpp : Implementation of CKeypad

#include "stdafx.h"
#include "Keypad.h"
#include "stdio.h"


using namespace std;

HMODULE	hMWXUSB = LoadLibrary(L"MWXUSB.DLL");

typedef unsigned char (OPEN_USB)(void);
OPEN_USB *lpopenusb=(OPEN_USB*)GetProcAddress(hMWXUSB, "Open_USB");

typedef unsigned char (ACCEPT_LED)(int data);
ACCEPT_LED *lpacceptled = (ACCEPT_LED*)GetProcAddress(hMWXUSB, "Accept_LED");


typedef void (SET_CALLBACK)(void ( __stdcall*lpfn)(int));
SET_CALLBACK *lpcallback=(SET_CALLBACK*)GetProcAddress(hMWXUSB, "Set_Callback");


static CKeypad* kp;
void __stdcall CB(int value)
{
	
}


STDMETHODIMP CKeypad::Open_USB()
{	
	unsigned char ucOpen= (*lpopenusb)();
	kp = this;
	(*lpacceptled)(1);
	(*lpcallback)(CB);
	return S_OK;
}


