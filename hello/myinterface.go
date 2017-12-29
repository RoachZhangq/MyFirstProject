package main

import(
	"fmt"
)

func main(){
	 s:=S{}
	 f(&s)

	 ss := SS{}
	 f(&ss) //ponter
	 f(ss)  //value
}

type I interface{
	Get() int
	Set(int)
}
type S struct{
	Age int
}
func (s S) Get() int{
	return s.Age
}
func (s *S) Set(age int ){
 	s.Age=age
}
func f(i I){
	i.Set(10)
	fmt.Println(i.Get())
}


type SS struct {
	Age int
}
func (s SS) Get() int {
	return s.Age
}
func (s SS) Set(age int) {
	s.Age = age
}
func f1(i I) {
	i.Set(10)
	fmt.Println(i.Get())
}