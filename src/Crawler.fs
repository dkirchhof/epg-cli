module Crawler

open FSharp.Data
open System.Collections.Generic
open Types

let getInnerText (node: HtmlNode) = node.InnerText()
let querySelectorAll selector (node: HtmlNode) = node.CssSelect selector
let querySelectorAllR selector (node: HtmlDocument) = node.CssSelect selector
let querySelector selector (node: HtmlNode) = node.CssSelect selector |> List.head
let querySelectorR selector (node: HtmlDocument) = node.CssSelect selector |> List.head

let nodeToChannel (node: HtmlNode) =
    {
        name = node |> getInnerText
        shows = List()
    }

let nodeToShow today (node: HtmlNode) = 
    {
        time = node |> querySelector ".time" |> getInnerText
        name = node |> querySelector ".name" |> getInnerText
        description = node |> querySelector ".description" |> getInnerText
        today = today
    }

let load (date: string) (limit: int) =
    (* let text = System.IO.File.ReadAllText("output2.html") *)
    (* let html = FSharp.Data.HtmlDocument.Parse(text) *)
    let html = HtmlDocument.Load($"https://www.tvtoday.de/programm/tv-guide/?column-index=0&limit={limit}&date={date}")

    let channels = html |> querySelectorAllR "#channel-slider > .slider > .slide > a > p" |> List.map nodeToChannel

    html |> querySelectorAllR ".js-time-span" |> List.iteri (fun timespanIndex timespan ->
        timespan |> querySelectorAll ".slide" |> List.iteri (fun channelIndex channel ->
            channel |> querySelectorAll ".tv-show-row > a > .text" |> List.iter (fun show ->
                let show' = nodeToShow (timespanIndex < 5) show

                channels[channelIndex].shows.Add show'
            ) 
        )
    )

    channels
