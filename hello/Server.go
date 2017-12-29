//server.go
package main

import(
	"fmt"
	"net/http"
)

func login(w http.ResponseWriter,r *http.Request){
	r.ParseForm()
	fmt.Println(r.Method)
    if r.Method=="GET"{
        fmt.Fprintf(w,"This is a GET request")
    }else{
        w.Header().Set("Access-Control-Allow-Origin", "*")
        fmt.Println("Recived info:",r.Form)
        fmt.Fprintf(w,r.Form.Get("info"))
    }
}
func main(){
	http.HandleFunc("/login",login)
	if err:=http.ListenAndServe(":9000",nil);err!=nil{
		fmt.Println("ListenAndServe err",err)
		
    }
}