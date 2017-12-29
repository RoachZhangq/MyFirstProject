 
package main
 import (  
  "fmt"  
  "strings" 
  "unicode"
 ) 
 
 func main() { 
  str :=[]string{"a","b","c"}
  fmt.Println(strings.Join(str,","))    
  fmt.Println(strings.EqualFold("A","a"))
  fmt.Println(strings.HasPrefix("A1231","A1")) 
  fmt.Println(strings.HasSuffix("A1231","1"))
  fmt.Println(strings.Contains("qwert","wer"))

  s1 := "中文"
  r1 := []rune(s1)
  fmt.Println(r1)

  var r rune=20013
  fmt.Println(r)
  fmt.Println(strings.ContainsRune("1中国",r))
  fmt.Println(strings.ContainsAny("我是中国人","不是"))
  fmt.Println(strings.Count("aebcdeee","ee"))
  fmt.Println(strings.Index("qwer","q"))
  var b byte=97
  fmt.Println(string(b))
  fmt.Println([]byte("a")[0])
  fmt.Println(strings.IndexByte("aqwera",97))
  fmt.Println(strings.IndexAny("qwer","r"))
  fmt.Println("-----")
  f:=func(c rune) bool{
      fmt.Println(c)
    return unicode.Is(unicode.Han, c)
  }
  fmt.Println(strings.IndexFunc("Hello,世界", f))
  fmt.Println("-----")
  fmt.Println(strings.IndexFunc("Hello,world", f))
  fmt.Println(strings.LastIndex("qwerqwer","r"))
  fmt.Println(strings.Title("hello,world"))
  fmt.Println(strings.ToLower("HELLO"))
  fmt.Println(strings.ToUpper("hello")) 
  fmt.Println(strings.Repeat("qwe",5))
  fmt.Println(strings.Replace("qwertqwert","q","A",1))

  f1:=func(r rune) rune{ 
      return r-32 
  }
  fmt.Println(strings.Map(f1,"abcdefg"))
  fmt.Println("********")
  f2 :=func (r rune) bool{
      var result bool=false 
      if r==97{
        result= true
        }
        fmt.Println(result)
     return result
  }
  fmt.Println(strings.TrimFunc("aqaqa aqwaera",f2))
  fmt.Println(strings.Fields(" abc def  ghi   qqq a "))
  
  f3:= func(r rune) bool{
      var result bool=false
      if r==44{   //逗号 ,
        result=true
      }
      return result
    }
    var arr1 []string=strings.FieldsFunc("qqq,ee ewww",f3)
    fmt.Println(arr1)
    fmt.Println(len(arr1))
    fmt.Println(strings.Split("q,,w,,e,r",",,"))
    fmt.Println(strings.SplitN("q,,w,,e,r",",,",-1))
    fmt.Println(strings.Join([]string{"q","w","e"},"&"))
  }