package main

import(
	"fmt"
	"reflect"
)

func main(){
	var funcArr []func(num int) int 
	//reflect.TypeOf(z)
	for	i:=0;i<3; i++{ 
		funcArr=append(funcArr,func(num int)int{
			if num==0{
				return (num+100)
			}else if num==1{
				return (num+200)
			}else{
				return (num+300)
			} 
		})
	}

	for	i,f:=range funcArr{
		fmt.Print(i,":")
		fmt.Println(f(i))
	}
	//回调 ---类似委托
	fmt.Println(Test(add,111,222,333))
}

type Callback1 func (nums []int) int 

func add(nums []int) int{
	var sum int
	for _,n:=range nums{
		sum+=n
	}
	return sum
}  

func Test(f Callback1,nums ...int) int{
	fmt.Print("nums ...int的类型是：")
	fmt.Println(reflect.TypeOf(nums))
	return f(nums)
}