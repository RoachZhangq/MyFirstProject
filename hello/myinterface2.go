package main

import(
	"fmt"
)
type Read1 interface{
	StartRead() string
}
type Write1 interface{
	StartWrite(str string) 
}
type Read1Write1 interface{
	Read1
	Write1
}
type AA struct{

}
func (a AA) StartRead() string {
	return "这里是StartRead的内容~"
}
func (a AA) StartWrite(str string)   {
   fmt.Println(str)
}
func main(){
	 var rw Read1Write1 =AA{} 
	 rw.StartWrite(rw.StartRead())
}