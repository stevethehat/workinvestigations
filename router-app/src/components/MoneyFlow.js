import Canvas from "../util/canvas"
import Box from "../util/box"

class MoneyFlow{
    constructor(){
        this.canvas = new Canvas("displayCanvas");

        this.setupColumns(3, 360);
        this.setupAreas();
    }

    setupColumns(count, width){
        var totalWidth = (count * width) + ((count -1) * 20);
        var margins = (this.canvas.displayCanvas.width - totalWidth) / 2;

        this.columns = [];

        var x = margins;
        for(var i = 0; i < count; i++){
            this.columns[i] = x;
            x = x + width + 20;
        }
        //debugger;
    }

    setupAreas(){
        const height = 80;
        var areas = this.areas;
        areas = {
            "capital": new Box(this.canvas, { x: this.columns[2], y: 10, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Capital" }),
            "fixedAssets": new Box(this.canvas, { x: this.columns[2], y: 200, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Fixed Assets" }),
            "sales": new Box(this.canvas, { x: this.columns[2], y: 400, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Sales" }),
            "invoicing": new Box(this.canvas, { x: this.columns[1], y: 700, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Invoicing" }),
            "turnover": new Box(this.canvas, { x: this.columns[0], y: 600, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Turnover" }),
            "grossProfit": new Box(this.canvas, { x: this.columns[0], y: 500, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Gross Profit" }),
            "expenses": new Box(this.canvas, { x: this.columns[0], y: 400, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Expenses" }),
        }
        areas["capital"].join(areas["fixedAssets"], "bl", "tl");
        areas["fixedAssets"].join(areas["sales"], "bc", "tc");
        areas["sales"].join(areas["invoicing"], "bl", "r");
        
    }
}

export default MoneyFlow;
