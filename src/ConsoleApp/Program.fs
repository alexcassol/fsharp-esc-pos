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
open Printer
 
[<EntryPoint>]
let main argv =
    printfn "Printing..."
 
    let docDef = { ColsNormal=42; ColsCondensed=56; ColsExpanded=24 }
 
    let prn = PrinterEscPos(docDef).Commands
     
    let doc = Document(prn)
    doc.NewLine 5
    
    doc.Append "test1"
    doc.Append "test2" 
    (*
    doc.Append (prn.ItalicText "test3")
    doc.Append (prn.Italic On)
    doc.Append "test4"
    doc.Append (prn.Italic Off)

    doc.Append prn.Center
    doc.Append "center text"
    doc.Append prn.Left 
    doc.Append (prn.Separator 30)
    doc.Append prn.OpenCloseTable
    doc.Append "test"
    doc.Append prn.LineTable
    doc.Append prn.OpenCloseTable
    

    doc.Append (prn.PaperCut Full)
 *)
    let p = Printer.Printer("DOCNAME")
    
    p.Print doc
    1