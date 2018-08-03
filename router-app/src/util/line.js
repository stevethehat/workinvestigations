class Line{
    constructor(canvas, from, to){
        this.canvas = canvas;
        const context = this.canvas.context;
        context.beginPath();
        context.moveTo(from.x, from.y);
        context.lineTo(to.x, to.y);
        context.stroke();

    }
}

export default Line;