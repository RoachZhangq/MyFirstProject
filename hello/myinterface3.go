package main

import
(
	"fmt"
	"strconv"
)
type Animal interface{

	Eat(s string)
}
type AnimalAttr struct{
	Name string
}
type Dog struct{
	AnimalAttr
	Age int
}
func (d Dog) Eat(s string) {
	fmt.Println("我是"+d.Name+",我"+strconv.Itoa(d.Age)+"岁了,正在吃:"+s)
}
type Cat struct{
	AnimalAttr
	Age int
}
func (c Cat) Eat(s string) {

	fmt.Println("我是"+c.Name+",我"+strconv.Itoa(c.Age)+"岁了,正在吃:"+s)
}

func Breeder(a Animal,s string){
	a.Eat(s)
} 

func main(){
	 var obj Dog=Dog{Age:1}
	 obj.Name="大黄狗"  
	 Breeder(obj,"骨头")
	 var obj2 Cat=Cat{Age:2}
	 obj2.Name="小花猫"
	 Breeder(&obj2,"鲫鱼")
}