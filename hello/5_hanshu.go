package main

import "fmt"

func main(){
	res:=plus(1,2)
	fmt.Println("1+2=",res)
}

func plus(a int, b int ) int{
	return a+b
}