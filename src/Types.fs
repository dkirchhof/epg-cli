module Types

open System.Collections.Generic

type Show = { time: string; today: bool; name: string; description: string; }
type Channel = { name: string; shows: List<Show>; }
