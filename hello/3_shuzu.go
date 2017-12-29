package main

import "fmt"

func main() {
	var a [5]int
	fmt.Println(a)

	a[4]=100
	fmt.Println(a)
	fmt.Println(a[4])

	fmt.Println("len:",len(a))

	b:=[5]int {1,2,3,4,5}
	fmt.Println("dcl:",b)

	var two [2][3] int
	for i:=0;i<len(two);i++{
		for j:=0;j<len(two[i]);j++{
			two[i][j]=i+j
		}
	}
	fmt.Println(two)

	c:=[...]int{1,2,3}
	fmt.Println(c)
}