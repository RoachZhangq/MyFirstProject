package main

import "fmt"
func main(){
	s:=student{id:1,name:"章强"}
	s.Print()
	fmt.Println(s)
}

type student struct {
	id int 
	name string
}
func (s *student) Print() {
	s.name="郑丽"
	fmt.Println(s.id,s.name)
}