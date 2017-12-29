package main

import "fmt"

func main(){
	m:=make(map[string]int)
	m["k1"]=11
	m["k2"]=22
	fmt.Println(m["k1"])
	fmt.Println(m["k2"])
	delete(m,"k1")
	fmt.Println(m)
	v1:=m["k2"]
	fmt.Println(v1)
	k1,v2:=m["k2"]
	fmt.Println(k1)
	fmt.Println(v2)
}
 