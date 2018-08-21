class Dot{
    constructor(canvas, position){
        var context = canvas.context;
        
        context.beginPath();
        context.arc(position.x, position.y, 2, 0, 2 * Math.PI, true);
        context.stroke();

    }
}

export default Dot;