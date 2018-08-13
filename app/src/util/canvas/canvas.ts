export class Canvas {
    public Context: CanvasRenderingContext2D;
    public DisplayCanvas: HTMLCanvasElement;
    /**
     */
    constructor(name: string) {
        this.DisplayCanvas = document.getElementById(name) as HTMLCanvasElement;
        this.DisplayCanvas.width = this.DisplayCanvas.getBoundingClientRect().width;
        this.DisplayCanvas.height = this.DisplayCanvas.getBoundingClientRect().height;

        this.Context = this.DisplayCanvas.getContext('2d') as CanvasRenderingContext2D;
    }
}

export interface IPoint {
    x: number;
    y: number;
}

