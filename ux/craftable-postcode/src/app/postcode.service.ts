import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { map } from 'rxjs';
import { AddressResult } from 'src/model/address';

@Injectable({
  providedIn: 'root'
})
export class PostcodeService {

  constructor(private httpClient: HttpClient) { }

  getAddress(postcode:string){
    let uri: string;
    let addressResult: AddressResult;
    uri = `https://api.postcodes.io/postcodes/${postcode}`;

    return this.httpClient.get<any>(uri).pipe(
        map((response: any) => {
            if (!response) {
                return addressResult;
            }
            return addressResult = response;
        })
    );
  }


  getAddressFromBack(postcode:string){
    let uri: string;
    let addressResult: AddressResult;
    uri = `https://localhost:7187/api/Address/${postcode}`;

    return this.httpClient.get<any>(uri).pipe(
        map((response: any) => {
            if (!response) {
                return addressResult;
            }
            return addressResult = response;
        })
    );
  }
}
