import { Canvas, Point } from './canvas';

export default class Line {
    protected Canvas: Canvas;
    constructor(canvas: Canvas, from: Point, to: Point) {
        this.Canvas = canvas;
        const context = this.Canvas.Context;
        context.beginPath();
        context.moveTo(from.x, from.y);
        context.lineTo(to.x, to.y);
        context.stroke();
    }
}

