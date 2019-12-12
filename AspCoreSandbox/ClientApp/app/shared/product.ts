
/** Tworzymy ścisły Typ dla produktu */
export interface Product {
    id: number;
    category: string;
    size: string;
    price: number;
    title: string;
    description: string;
    dateOfProduceStart: Date;
    dateOfProduceEnd: Date;
    countryOfOrigin: string;
}