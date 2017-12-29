package main
import "fmt"

func myfunc() func() int{
	i:=0
	return func() int{
		fmt.Println("此时的i：",i)
		i+=1
		return i
	}
}
func main(){
	nextInt:=myfunc();
	fmt.Println(nextInt())
	fmt.Println(nextInt())
	fmt.Println(nextInt())
	fmt.Println(myfunc()())

	var fs [] func(a int)int
	for i:=0;i<3; i++{ 
		fs=append(fs,func(a int )int{
			return a
		})
	}

	for i,f:=range fs{
		fmt.Println(i)
		fmt.Println("%p = %v\n",f,f(i))
	}

	sumFunc:=adder();
	for i:=0;i<10;i++{ 
		value:=sumFunc(i)
		if i==9 {
			fmt.Println("累加：",value) 
		}
		
	}
}

func adder() func(int) int{
	sum:=0;
	return func(x int )int {
		sum+=x
		return sum
	}
}