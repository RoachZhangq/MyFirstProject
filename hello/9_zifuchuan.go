package main

import (
	"fmt"
	s "strings"
)
 

var p=fmt.Println
func main(){
	p("Contains",s.Contains("test","es"))
	p("count",s.Count("abcda","a")) 
	p("HasPrefix: ", s.HasPrefix("test", "tes"))
	p("HasSuffix: ", s.HasSuffix("test", "st"))
	p("Index:     ", s.Index("test", "e"))
	p("Join:      ", s.Join([]string{"a", "b","C"}, "-"))
	p("Repeat:    ", s.Repeat("ab,", 10))
	p("Replace:   ", s.Replace("foo", "o", "A",-1))
	p("Split:     ", s.Split("a-b-c-d-e", "-"))
	p("ToLower:   ", s.ToLower("TEST"))
	p("ToUpper:   ", s.ToUpper("test"))
	p()
	p("len",len("abcda"))
	p("Char:", "hAllo"[1])
	p(s.Split("hello","")[1])
}