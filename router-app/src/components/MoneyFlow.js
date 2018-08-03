import Canvas from "../util/canvas"
import Box from "../util/box"

class MoneyFlow{
    constructor(){
        this.canvas = new Canvas("displayCanvas");

        this.setupHSlots(3, 360);
        this.setupAreas();
    }

    setupHSlots(count, width){
        var totalWidth = (count * width) + ((count -1) * 20);
        var margins = (this.canvas.displayCanvas.width - totalWidth) / 2;

        this.hSlots = [];

        var x = margins;
        for(var i = 0; i < count; i++){
            this.hSlots[i] = x;
            x = x + width + 20;
        }
    }

    setupAreas(){
        const height = 80;
        var areas = this.areas;
        areas = {
            "capital": new Box(this.canvas, { x: this.hSlots[2], y: 10, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Capital" }),
            "fixedAssets": new Box(this.canvas, { x: this.hSlots[2], y: 400, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Fixed Assets" }),
            "sales": new Box(this.canvas, { x: this.hSlots[2], y: 600, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Sales" }),
            "invoicing": new Box(this.canvas, { x: this.hSlots[1], y: 800, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Invoicing" }),
            "turnover": new Box(this.canvas, { x: this.hSlots[0], y: 600, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Turnover" }),
            "grossProfit": new Box(this.canvas, { x: this.hSlots[0], y: 500, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Gross Profit" }),
            "expenses": new Box(this.canvas, { x: this.hSlots[0], y: 400, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Expenses" }),
            "operatingProfit": new Box(this.canvas, { x: this.hSlots[0], y: 300, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Operating Profit" }),
            "interest": new Box(this.canvas, { x: this.hSlots[0], y: 200, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Interest" }),
            "tax": new Box(this.canvas, { x: this.hSlots[0], y: 100, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Tax" }),
            "netProfit": new Box(this.canvas, { x: this.hSlots[0], y: 0, width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Net Profit" })
        }
        areas["capital"].join(areas["fixedAssets"], "bl", "tl");
        areas["fixedAssets"].join(areas["sales"], "bc", "tc");
        areas["sales"].join(areas["invoicing"], "bl", "r");
        areas["invoicing"].join(areas["turnover"], "l", "br");
        areas["turnover"].join(areas["grossProfit"], "tc", "bc");
        areas["grossProfit"].join(areas["expenses"], "tc", "bc");
        areas["expenses"].join(areas["operatingProfit"], "tc", "bc");
        
    }
}

export default MoneyFlow;
