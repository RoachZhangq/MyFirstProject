package main

import(
	"fmt"
	
)
func init(){
	fmt.Println("这里是init1方法")
}
func init(){
	fmt.Println("这里是init2方法")
}
type XZ interface{
	GetName()  
}
type Y struct{
	name string
}
type Z struct{
	Name string 
}
func (y Y) GetName1(){
	fmt.Println(y.name)
}
func (y Y) GetName(){
	fmt.Println(y.name)
}
func (z Z) GetName1(){
	fmt.Println(z.Name)
}
func (z Z) GetName(){
	fmt.Println(z.Name)
}

func use( o XZ){
	o.GetName() 
}
func main(){
	 y1:=Y{name:"○"}
	 y1.GetName1()
	 use(y1)
	z1:=Z{Name:"口"}
	z1.GetName1()
	use(z1)
}