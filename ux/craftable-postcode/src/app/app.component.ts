import {Component} from '@angular/core';
import {FormControl} from '@angular/forms';
import {Observable, Subject} from 'rxjs';
import {startWith, map} from 'rxjs/operators';
import { PostcodeService } from './postcode.service';
import { AddressView } from 'src/model/addressView';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  pageTitle = 'craftable-postcode';
  control = new FormControl('');
  streets: string[] = ['PO63TD', 'W1B3AG', 'SW1A', 'SW46TA'];
  filteredStreets: Observable<string[]> | undefined;
  postcode: string | undefined;
  title: string | undefined;
  description: string | undefined;
  content: string | undefined;
  addresses: AddressView[] = [];
  error: boolean = false;
  messageError: string = "";

  protected readonly onDestroy = new Subject<void>();


  constructor(private postcodeService: PostcodeService){

  }

  ngOnInit() {
    this.error = false; 
    this.filteredStreets = this.control.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }

  private _filter(value: string): string[] {
    const filterValue = this._normalizeValue(value);
    return this.streets.filter(street => this._normalizeValue(street).includes(filterValue));
  }

  private _normalizeValue(value: string): string {
    return value.toLowerCase().replace(/\s/g, '');
  }

  searchAddress(){
    console.log(this.postcode, "hellooo")
    if(this.postcode){
      this.postcodeService
      .getAddressFromBack(this.postcode)
      .subscribe({
        next: address => {
          this.fillError({status:false,message:""});
          this.addresses = address;
          console.log(address);
        },
        error: error => {
          this.fillError({status:true,message:error.message});
          console.log(error,"error")
        }  
      })
    }    
  }

  private fillError(error: any) {
    this.error = error.status;
    this.messageError = error.message;
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.unsubscribe();
  }
}
