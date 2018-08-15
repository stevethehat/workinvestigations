class Grid{
    constructor(canvas, horrizontal, vertical, width, height, hMargin, vMargin){
        this.canvas = canvas;
        this.vertical = vertical;
        this.horrizontal = horrizontal;
        this.width = width;
        this.height = height;
        this.hMargin = hMargin;
        this.vMargin = vMargin;

        this.setupHSlots();
        this.setupVSlots();
    }
    setupHSlots(){
        var totalWidth = (this.horrizontal * this.width) + ((this.horrizontal -2) * this.hMargin);
        var margins = (this.canvas.displayCanvas.width - totalWidth) / 2;

        this.hSlots = [];

        var x = margins;
        for(var i = 0; i < this.horrizontal; i++){
            this.hSlots[i] = x;
            x = x + this.width + this.hMargin;
        }
    }

    setupVSlots(){
        //debugger;
        var totalHeight = (this.vertical * this.height) + ((this.vertical -1) * this.vMargin);
        var margins = (this.canvas.displayCanvas.height - totalHeight) / 2;

        this.vSlots = [];

        var y = margins;
        for(var i = 0; i < this.vertical; i++){
            this.vSlots[i] = y;
            y = y + this.height + this.vMargin;
        }
    }

}

export default Grid;