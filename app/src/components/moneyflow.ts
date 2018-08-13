import {Canvas} from '@/util/canvas/canvas';
import {Box, IBoxDefinition} from '@/util/canvas/box';
import Grid from '@/util/canvas/grid';

export default class MoneyFlow {
    protected Canvas: Canvas;
    protected Grid: Grid;
    protected Areas: any;

    constructor() {
        this.Canvas = new Canvas('displayCanvas');

        this.Grid = new Grid(this.Canvas, 3, 9, 360, 80, 20, 30);
        this.setupAreas();
    }

    public setupAreas() {
        const height: number = this.Grid.Height;
        const width: number = this.Grid.Width;
        let areas = this.Areas;
        areas = {
            capital: this.GetBox(2, 0, 'Capital'),
            fixedAssets: this.GetBox(2, 2, 'Fixed Assets'),
            currentAssets: this.GetBox(2, 4, 'Current Assets'),
            sales: this.GetBox(2, 6, 'Sales'),
            invoicing: this.GetBox(1, 7, 'Invoicing'),
            turnover: this.GetBox(0, 6, 'Turnover'),
            grossProfit: this.GetBox(0, 5, 'Gross Profit'),
            expenses: this.GetBox(0, 4, 'Expenses'),
            operatingProfit: this.GetBox(0, 3, 'Operating Profit'),
            interest: this.GetBox(0, 2, 'Interest'),
            tax: this.GetBox(0, 1, 'Tax'),
            netProfit: this.GetBox(0, 0, 'Net Profit'),
        };

        areas.capital.join(areas.fixedAssets, 'bl', 'tl')
        .join(areas.currentAssets, 'bc', 'tc')
        .join(areas.sales, 'bc', 'tc')
        .join(areas.invoicing, 'bl', 'r')
        .join(areas.turnover, 'l', 'br')
        .join(areas.grossProfit, 'tc', 'bc')
        .join(areas.expenses, 'tc', 'bc')
        .join(areas.operatingProfit, 'tc', 'bc')
        .join(areas.interest, 'tc', 'bc')
        .join(areas.tax, 'tc', 'bc')
        .join(areas.netProfit, 'tc', 'bc');


    }

    protected GetBox(hSlot: number, vSlot: number, title: string): Box {
        const x: number = this.Grid.HSlots[hSlot];
        const y: number = this.Grid.VSlots[vSlot];
        const width: number = this.Grid.Width;
        const height: number = this.Grid.Height;
        const definition: IBoxDefinition = {
            x, y,
            Width: width, Height: height,
            LineColor: 'black', LineWidth: 1,
            Title: title,
        };
        const result: Box = new Box(this.Canvas, definition);

        return result;
    }
}
