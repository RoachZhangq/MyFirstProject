package main

import "fmt"
import "math"

type geometry interface{
	getName()string
	mianji() float64
	zhouchang() float64
}
type zfx struct{
	width,height float64
	name string
}
type yuan struct{
	banjin float64
	name string
}
func (z zfx) mianji() float64 {
	return z.width*z.height
}
func (z zfx) zhouchang() float64{
	return (z.width+z.height)*2
}
func (z zfx) getName() string{
	return z.name
}

func (y yuan) mianji() float64{
	return math.Pi*y.banjin*y.banjin
}
func (y yuan) zhouchang() float64{
	return 2*y.banjin*math.Pi
}
func (y yuan) getName() string{
	return y.name
}
func geometry_use(g geometry){
	fmt.Println("形状:",g.getName())
	fmt.Println("g:",g)
	fmt.Println("面积:",g.mianji())
	fmt.Println("周长：",g.zhouchang())
}
func main(){
	zhengfangxing:=zfx{width:5,height:2,name:"长方形"}
	yuanxing:= yuan{banjin:10,name:"圆形"}
	geometry_use(zhengfangxing)
	geometry_use(yuanxing)
}