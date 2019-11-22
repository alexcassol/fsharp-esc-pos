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
 
open System.Collections.Generic

module Lists =

    type SimpleList<'a> () =
        let lst = new List<'a>()

        member x.Enqueue (item: 'a) = lst.Add item |> ignore

        member x.TryGet () =
            match x.Count with
            | 0 -> None
            | _ -> let item = lst |> Seq.head
                   lst.RemoveAt 0
                   Some item

        member x.Count with get () = lst.Count

        member x.Clear () = lst.Clear


    type SimpleQueue<'a> () =
        let queue = new LinkedList<'a>()

        /// Add an item to the tail of the queue.
        member x.Enqueue (item: 'a) = queue.AddLast item |> ignore

        /// Get the item at the head of the queue and remove it, the result is None if the queue is empty.
        member x.TryDequeue () =
            match x.Count with
            | 0 -> None
            | _ -> let item = queue.First.Value
                   queue.RemoveFirst ()
                   Some item

        /// The number of items currently in the queue.
        member x.Count with get () = queue.Count

        member x.Clear () = queue.Clear

        member x.GetAll () =            
            seq {
                while queue.Count > 0 do                    
                    let item = x.TryDequeue()
                    if item.IsSome then
                        yield item.Value 
            }
            
            
