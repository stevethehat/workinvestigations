import Canvas from "../util/canvas"
import Box from "../util/box"
import Grid from "../util/grid"

class MoneyFlow{
    constructor(){
        this.canvas = new Canvas("displayCanvas");

        this.grid = new Grid(this.canvas, 3, 9, 360, 80, 20, 30);
        this.setupAreas();
    }

    
    setupAreas(){
        const height = this.grid.height;
        var areas = this.areas;
        areas = {
            "capital": new Box(this.canvas, { x: this.grid.hSlots[2], y: this.grid.vSlots[0], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Capital" }),
            "fixedAssets": new Box(this.canvas, { x: this.grid.hSlots[2], y: this.grid.vSlots[2], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Fixed Assets" }),
            "currentAssets": new Box(this.canvas, { x: this.grid.hSlots[2], y: this.grid.vSlots[4], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Current Assets" }),
            "sales": new Box(this.canvas, { x: this.grid.hSlots[2], y: this.grid.vSlots[6], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Sales" }),
            "invoicing": new Box(this.canvas, { x: this.grid.hSlots[1], y: this.grid.vSlots[7], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Invoicing" }),
            "turnover": new Box(this.canvas, { x: this.grid.hSlots[0], y: this.grid.vSlots[6], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Turnover" }),
            "grossProfit": new Box(this.canvas, { x: this.grid.hSlots[0], y: this.grid.vSlots[5], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Gross Profit" }),
            "expenses": new Box(this.canvas, { x: this.grid.hSlots[0], y: this.grid.vSlots[4], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Expenses" }),
            "operatingProfit": new Box(this.canvas, { x: this.grid.hSlots[0], y: this.grid.vSlots[3], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Operating Profit" }),
            "interest": new Box(this.canvas, { x: this.grid.hSlots[0], y: this.grid.vSlots[2], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Interest" }),
            "tax": new Box(this.canvas, { x: this.grid.hSlots[0], y: this.grid.vSlots[1], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Tax" }),
            "netProfit": new Box(this.canvas, { x: this.grid.hSlots[0], y: this.grid.vSlots[0], width: 300, height: height, lineColor: "black", lineWidth: 1, title: "Net Profit" })
        }

        areas["capital"].join(areas["fixedAssets"], "bl", "tl")
        .join(areas["currentAssets"], "bc", "tc")
        .join(areas["sales"], "bc", "tc")
        .join(areas["invoicing"], "bl", "r")
        .join(areas["turnover"], "l", "br")
        .join(areas["grossProfit"], "tc", "bc")
        .join(areas["expenses"], "tc", "bc")
        .join(areas["operatingProfit"], "tc", "bc")
        .join(areas["interest"], "tc", "bc")
        .join(areas["tax"], "tc", "bc")
        .join(areas["netProfit"], "tc", "bc")

        
    }
}

export default MoneyFlow;
