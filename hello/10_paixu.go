package main

import "fmt"
import "sort"

type ByLength []string

func (s ByLength) Len() int {
	return len(s)
}
func (s ByLength) Swap(i, j int) {
	s[i], s[j] = s[j], s[i]
}
func (s ByLength) Less(i, j int) bool {
	return len(s[i]) > len(s[j])
}
func main(){
	a:=[]string{"a","bb","ccc","eeee"}
	sort.Sort(ByLength(a))
	fmt.Println(a)
}