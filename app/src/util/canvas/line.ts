import { Canvas, IPoint } from '@/util/canvas/canvas';

export default class Line {
    protected canvas: Canvas;
    constructor(canvas: Canvas, from: IPoint, to: IPoint) {
        this.canvas = canvas;
        const context = this.canvas.context;
        context.beginPath();
        context.moveTo(from.x, from.y);
        context.lineTo(to.x, to.y);
        context.stroke();
    }
}

