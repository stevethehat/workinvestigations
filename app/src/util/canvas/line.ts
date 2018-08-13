import { Canvas, IPoint } from '@/util/canvas/canvas';

export default class Line {
    constructor(readonly canvas: Canvas, from: IPoint, to: IPoint) {
        const context: CanvasRenderingContext2D = this.canvas.context;
        context.beginPath();
        context.moveTo(from.x, from.y);
        context.lineTo(to.x, to.y);
        context.stroke();
    }
}

