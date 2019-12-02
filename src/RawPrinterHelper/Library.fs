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
open CommonLibrary
open System

module Printer =
    open System
    open System.IO
    open System.Runtime.InteropServices
    open Native
    open System.ComponentModel

    let private getErrorMessage =
        let errorMessage = Win32Exception(Marshal.GetLastWin32Error())
        errorMessage.Message
        
    let private defineDocDataType =        
        match CommonLibrary.SysOp.getWindowsVersion with
        | x, y when x >= 6 && y >= 2 -> "XPS_PASS"
        | x, _ when x > 6 -> "RAW"
        | _, _ -> "RAW"

    let private getPrinterDriverInfo hPrinter =
        let mutable driverInfo = new IntPtr(0)
        let mutable buf_len = 0;

        let a = GetPrinterDriver(hPrinter, "", 8, driverInfo, 0, &buf_len);

        driverInfo <- Marshal.AllocHGlobal(buf_len);

        let _ = GetPrinterDriver(hPrinter, "", 8, driverInfo, buf_len, &buf_len);
        
        let info = Marshal.PtrToStructure(driverInfo, typeof<DRIVER_INFO_8>)
        
        Marshal.FreeHGlobal(driverInfo)
        Conversions.TryCast<DRIVER_INFO_8>(info)

    
    let private sendToPrinter szPrinterName pBytes dwCount =
        let mutable dwWritten:int32 = 0
        let mutable hPrinter = new IntPtr(0)
        let mutable di = DOCINFOA(pDocName = CommonLibrary.Utils.randomStr 8, pDataType = defineDocDataType, pOutputFile = null)
        let mutable pd = PRINTER_DEFAULTS(pDatatype=IntPtr.Zero, pDevMode=IntPtr.Zero, DesiredAccess=0)
        let mutable bSuccess = false
        
        if OpenPrinter(szPrinterName, &hPrinter, &pd) then
            if StartDocPrinter(hPrinter, 1, &di) then
                if StartPagePrinter(hPrinter) then
                    if WritePrinter(hPrinter, pBytes, dwCount, &dwWritten) then
                        bSuccess <- true
                    EndPagePrinter(hPrinter) |> ignore                        
                EndDocPrinter(hPrinter) |> ignore 
            ClosePrinter(hPrinter) |> ignore 

        match bSuccess with
        | true -> 0
        | _ -> failwith getErrorMessage
 
    
    let SendBytesToPrinter (szPrinterName: string, data: byte[]) =
        let len = Array.length data
        let mutable pUnmanagedBytes = new IntPtr(0)
        
        pUnmanagedBytes <- Marshal.AllocCoTaskMem len
        Marshal.Copy(data, 0, pUnmanagedBytes, len)   

        let retVal = sendToPrinter szPrinterName pUnmanagedBytes len
        
        Marshal.FreeCoTaskMem(pUnmanagedBytes)        
        retVal

    let SendStringToPrinter (szPrinterName: string, szString: string) =
        // How many characters are in the string?
        let dwCount = (szString.Length + 1) * Marshal.SystemMaxDBCSCharSize;
        
        // Assume that the printer is expecting ANSI text, and then convert
        // the string to ANSI text.
        let pBytes = Marshal.StringToCoTaskMemAnsi(szString)

        // Send the converted ANSI string to the printer.
        let retVal = sendToPrinter szPrinterName pBytes dwCount
        
        Marshal.FreeCoTaskMem(pBytes)
        
        retVal
    
    let SendAsciiToPrinter (szPrinterName: string, szString: string) =
        let enc = System.Text.Encoding.ASCII.GetBytes szString
        //if  you are using UTF-8 and get wrong values in qrcode printing, you must use ASCII instead.
        //let enc = System.Text.Encoding.UTF8.GetBytes(data)        
        let retVal = SendBytesToPrinter(szPrinterName, enc)
        retVal

    let SendFileToPrinter (szPrinterName: string, szFileName: string) =
        use stream = File.Open(szFileName, FileMode.Open, FileAccess.Read)
        use mem = new MemoryStream()
        stream.CopyTo mem
        let data = mem.ToArray()
        let len = Array.length data

        // Allocate some unmanaged memory for those bytes.
        let pUnmanagedBytes = Marshal.AllocCoTaskMem(len)
        Marshal.Copy(data, 0, pUnmanagedBytes, len)   
        let retVal = sendToPrinter szPrinterName pUnmanagedBytes len

        Marshal.FreeCoTaskMem(pUnmanagedBytes) 
        retVal
         




        
    

