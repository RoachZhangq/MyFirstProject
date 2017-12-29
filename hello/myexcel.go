package main

import(
	"fmt"
	"os"
	"github.com/xuri/excelize"
)

func main(){
	xlsx, err := excelize.OpenFile("./Workbook.xlsx") 
    if err != nil {
		fmt.Println("出错了")
        fmt.Println(err)
        os.Exit(1)
    }
    // Get value from cell by given worksheet name and axis.
    cell := xlsx.GetCellValue("Sheet1", "B2")
    fmt.Println(cell)
    // Get all the rows in the Sheet1.
    rows := xlsx.GetRows("Sheet1")
    for _, row := range rows {
        for _, colCell := range row {
            fmt.Print(colCell, "\t")
        }
        fmt.Println()
    }
}