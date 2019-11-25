//The MIT License (MIT)
//Copyright (c) 2019 Alex Cassol
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights to
//use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//the Software, and to permit persons to whom the Software is furnished to do so,
//subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
//FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//DEALINGS IN THE SOFTWARE.

namespace RawPrinterHelper
open System

module private Native =
    open System.Runtime.InteropServices

    [<StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)>]
    [<Struct>]
    type DOCINFOA =        
        [<MarshalAs(UnmanagedType.LPStr)>] val mutable pDocName : string
        [<MarshalAs(UnmanagedType.LPStr)>] val mutable pOutputFile : string
        [<MarshalAs(UnmanagedType.LPStr)>] val mutable pDataType : string
    
    [<StructLayout(LayoutKind.Sequential)>]
    [<Struct>]
    type  PRINTER_DEFAULTS =
        val mutable pDatatype : IntPtr
        val mutable pDevMode : IntPtr
        val mutable DesiredAccess : int 

    [<Literal>]
    let DllName = "winspool.Drv"
    
    [<DllImport(DllName, EntryPoint="OpenPrinterA", SetLastError=true, CharSet=CharSet.Ansi, ExactSpelling=true,
                CallingConvention=CallingConvention.StdCall)>]
    extern bool OpenPrinter([<MarshalAs(UnmanagedType.LPStr)>] string pPrinterName, IntPtr& phPrinter, PRINTER_DEFAULTS& pDefault)
        
    [<DllImport(DllName, EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)>]
    extern bool ClosePrinter(IntPtr hPrinter)

    [<DllImport(DllName, EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi,
            ExactSpelling = true, CallingConvention = CallingConvention.StdCall)>]
    extern bool StartDocPrinter(IntPtr hPrinter, int level, DOCINFOA& di)

    [<DllImport(DllName, EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)>]
    extern bool EndDocPrinter(IntPtr hPrinter)

    [<DllImport(DllName, EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)>]
    extern bool StartPagePrinter(IntPtr hPrinter)

    [<DllImport(DllName, EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)>]
    extern bool EndPagePrinter(IntPtr hPrinter)

    [<DllImport(DllName, EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)>]
    extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, int& dwWritten)

    [<DllImport(DllName, EntryPoint = "FlushPrinter", SetLastError = true, CharSet = CharSet.Ansi, 
            ExactSpelling = true, CallingConvention = CallingConvention.StdCall)>]
    extern bool FlushPrinter(IntPtr hPrinter, IntPtr pBuf, int cbBuf, int32& pcWritten, int&  cSleep)

    [<DllImport(DllName, EntryPoint = "SetJobA", SetLastError = true)>]
    extern int SetJob(nativeint hPrinter, uint32 JobId, uint32 Level, IntPtr pJob, uint32 Command_Renamed)
 
    






