import {Canvas} from '@/util/canvas/canvas';

export default class Grid {
    public width: number;
    public height: number;
    public vSlots: number[];
    public hSlots: number[];
    protected canvas: Canvas;
    protected vertical: number;
    protected horrizontal: number;
    protected hMargin: number;
    protected vMargin: number;

    constructor(
        canvas: Canvas,
        horrizontal: number, vertical: number,
        width: number, height: number,
        hMargin: number, vMargin: number) {

        this.canvas = canvas;
        this.vertical = vertical;
        this.horrizontal = horrizontal;
        this.width = width;
        this.height = height;
        this.hMargin = hMargin;
        this.vMargin = vMargin;
        this.vSlots = [];
        this.hSlots = [];

        this.setupHSlots();
        this.setupVSlots();
    }

    public setupHSlots() {
        const totalWidth: number = (this.horrizontal * this.width) + ((this.horrizontal - 2) * this.hMargin);
        const margins: number = (this.canvas.displayCanvas.width - totalWidth) / 2;

        let x: number = margins;
        for (let i = 0; i < this.horrizontal; i++) {
            this.hSlots[i] = x;
            x = x + this.width + this.hMargin;
        }
    }

    public setupVSlots() {
        // debugger;
        const totalHeight: number = (this.vertical * this.height) + ((this.vertical - 1) * this.vMargin);
        const margins: number = (this.canvas.displayCanvas.height - totalHeight) / 2;

        let y: number = margins;
        for (let i = 0; i < this.vertical; i++) {
            this.vSlots[i] = y;
            y = y + this.height + this.vMargin;
        }
    }
}
