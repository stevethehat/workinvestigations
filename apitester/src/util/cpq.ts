export class CPQWholegood{
    public Id                       : number;
    public ConfigurationId          : string;
    public PO                       : string;
    public Make                     : string;
    public Series                   : string;
    public Model                    : string;
    public Quantity                 : number;
    public PriceBeforeVat           : number;
    public RecommendedRetailPrice   : number;
    public DealerDiscount           : number;
    public WarrantyDescription      : string;
    public WarrantyCost             : number;

    constructor() {
        this.Id                     = 0;
        this.ConfigurationId        = 'CID1';
        this.PO                     = '104077';
        this.Make                   = 'Challenger';
        this.Series                 = 'AG';
        this.Model                  = 'AG-010';
        this.Quantity               = 1;
        this.PriceBeforeVat         = 56000;
        this.RecommendedRetailPrice = 96000;
        this.DealerDiscount         = 40000;
        this.WarrantyDescription    = 'no warranty';
        this.WarrantyCost           = 5000;
    }

    getCPQData(orderType: string, fulFilmentType: string): any{
        const result = {
            configrationId          : this.ConfigurationId,
            brand                   : this.Make,
            series                  : this.Series,
            model                   : this.Model,
            totalPriceWithoutVAT    : this.PriceBeforeVat,
            listPrice               : this.RecommendedRetailPrice,
            quantity                : this.Quantity,    
            currency                : "GBP",
            vatPercent              : 20,
            dealerDiscount          : this.DealerDiscount,
            orderType               : orderType,
            orderFulFilmentType     : fulFilmentType
        }

        return result;
    }
}

export class CPQTradeIn{
    public Id                   : number;
    public SerialNumber         : string;
    public Make                 : string;
    public Model                : string;
    public Description          : string;
    public Condition            : string;
    public CompensationValue    : number;
    public BookValue            : number;
    public YearOfReg            : number;
    public Clock                : number;
    
    constructor() {
        this.Id                 = 0;
        this.SerialNumber       = '311660';
        this.Make               = 'KR';
        this.Model              = 'RES85/205';
        this.Description        = 'KRONEVATOR (0029)';
        this.Condition          = 'a few dents';
        this.CompensationValue  = 2600;
        this.BookValue          = 2700;
        this.YearOfReg          = 1992;
        this.Clock              = 10000;
    }

    getCPQData(){
        const result = {
            serialNumber        : this.SerialNumber,
            brand               : this.Make,
            model               : this.Model,
            modelVariant        : this.Description,
            compensationValue   : this.CompensationValue,
            bookValue           : this.BookValue,
            machineCondition    : this.Condition,
            yearOfReg           : this.YearOfReg,
            runningHours        : this.Clock
        }   
        return result; 
    }
}

//Comment = $"{attachment.Name} - {attachment.Description} [{attachment.ConfigurationId}] {attachment.Price:c}".Replace("£", ""),
export class CPQAdditionalItem{
    public Id?              : number;
    public Name?            : string;
    public Description?     : string;
    public PartId           : string;
    public Price            : number;

    constructor(){
        this.Id             = 0;
        this.Name           = '';
        this.Description    = '';
        this.PartId         = '';
        this.Price          = 0;
    }

    getCPQData(){
        const result = {
            configurationId : this.PartId,
            name            : this.Name,
            description     : this.Description,
            price           : this.Price,
            cost            : this.Price,
            vatPercent      : 20
        }   
        return result; 
    }
}
