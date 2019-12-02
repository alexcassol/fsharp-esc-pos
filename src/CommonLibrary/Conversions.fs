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

namespace CommonLibrary  

module Conversions =
    open System

    let ToByteArray arr = 
        seq { for x in [0..(Array.length arr) - 1] do yield  byte arr.[x] }
        |> Seq.toArray 

    let ToByte : string -> byte array = 
        fun s -> System.Text.Encoding.ASCII.GetBytes(s) 

    let TryCast<'a> o =
        match box o with
        | :? 'a as output -> Some output
        | _ -> None
  
    let TupleToByteArray tup =
        tup
        |> List.map ( fun m -> Byte.Parse(m))
        |> List.toArray
  