export class Canvas {
    public context: CanvasRenderingContext2D;
    public displayCanvas: HTMLCanvasElement;
    /**
     */
    constructor(name: string) {
        this.displayCanvas = document.getElementById(name) as HTMLCanvasElement;
        this.displayCanvas.width = this.displayCanvas.getBoundingClientRect().width;
        this.displayCanvas.height = this.displayCanvas.getBoundingClientRect().height;

        this.context = this.displayCanvas.getContext('2d') as CanvasRenderingContext2D;
    }
}

export interface IPoint {
    x: number;
    y: number;
}

