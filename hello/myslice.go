package main 

import(
	f "fmt"
	_ "reflect"
)

func main(){
   arr:=[10] int{0,1,2,3,4,5,6,7,8,9}

   slice_a:=arr[2:9]

   f.Println(slice_a)
   f.Println(len(slice_a))
   f.Println(cap(slice_a))

   slice_b:=slice_a[2:6]
   f.Println(slice_b)
   f.Println(len(slice_b))
   f.Println(cap(slice_b))
   
 /*   slice_c :=append(slice_a,slice_b...)
   f.Println(slice_c)
   f.Println(len(slice_c))
   f.Println(cap(slice_c))
	 */
/*    copy(slice_a,slice_b)
   f.Println(slice_a)
   f.Println(len(slice_a))
   f.Println(cap(slice_a)) */

  /*  slice_c:= removeSliceByIndex(slice_a,2)
   f.Println(slice_c)
   f.Println(len(slice_c))
   f.Println(cap(slice_c))
   f.Println(reflect.TypeOf(slice_c)) */

 /*   slice_d:= append(slice_a[:2], slice_a[3:]...)
   f.Println(slice_d)
   f.Println(len(slice_d))
   f.Println(cap(slice_d)) */

   slice_e:= append(slice_a , 100)
   f.Println(slice_e)
   f.Println(len(slice_e))
   f.Println(cap(slice_e)) 

}

func removeSliceByIndex(s1 []int,i int) []int{
	s1= append(s1[:i], s1[i+1:]...)
	return s1
}