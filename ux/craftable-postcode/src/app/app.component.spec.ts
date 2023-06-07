import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { PostcodeService } from './postcode.service';
import { Observable, of } from 'rxjs';
import { AddressView } from 'src/model/addressView';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatAutocompleteModule } from '@angular/material/autocomplete';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let postcodeService: PostcodeService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule, ReactiveFormsModule, HttpClientModule, MatToolbarModule, MatAutocompleteModule],
      declarations: [AppComponent],
      providers: [PostcodeService]
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    postcodeService = TestBed.inject(PostcodeService);

    fixture.detectChanges();
  });

  afterEach(() => {
    fixture.destroy();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  xit('should initialize the component properties', () => {
    expect(component.pageTitle).toBe('craftable-postcode');
    expect(component.control).toBeTruthy();
    expect(component.streets).toEqual(['PO63TD', 'W1B3AG', 'SW1A', 'SW46TA']);
    expect(component.filteredStreets).toBeUndefined();
    expect(component.postcode).toBeUndefined();
    expect(component.title).toBeUndefined();
    expect(component.description).toBeUndefined();
    expect(component.content).toBeUndefined();
    expect(component.addresses).toEqual([]);
    expect(component.error).toBeFalse();
    expect(component.messageError).toBe('');
  });

  xit('should filter streets based on input value', () => {
    component.control.setValue('W1');
    component.filteredStreets?.subscribe((filtered) => {
      expect(filtered).toEqual(['W1B3AG']);
    });

    component.control.setValue('SW');
    component.filteredStreets?.subscribe((filtered) => {
      expect(filtered).toEqual(['SW1A', 'SW46TA']);
    });

    component.control.setValue('XYZ');
    component.filteredStreets?.subscribe((filtered) => {
      expect(filtered).toEqual([]);
    });
  });

  it('should normalize the input value', () => {
    const normalizedValue = component['_normalizeValue']('  TesT ValUe ');
    expect(normalizedValue).toBe('testvalue');
  });

  it('should search for addresses', () => {
    const postcode = '12345';
    const mockAddresses: AddressView[] = [
      // Mocked addresses
      // Adjust the properties and values as per your needs
      { title: 'Address 1', description: 'Description 1', content: 'Content 1', id: 0, latitude: 51.560414, longitude: -0.116805, postCode:"N76RS"  },
      { title: 'Address 2', description: 'Description 2', content: 'Content 2', id: 0, latitude: 50.846019, longitude: -1.086124, postCode:"PO63TD"  }
    ];

    spyOn(postcodeService, 'getAddressFromBack').and.returnValue(of(mockAddresses));

    component.postcode = postcode;
    component.searchAddress();

    expect(postcodeService.getAddressFromBack).toHaveBeenCalledWith(postcode);
    expect(component.error).toBeFalse();
    expect(component.messageError).toBe('');
    expect(component.addresses).toEqual(mockAddresses);
  });

  it('should handle search error', () => {
    const postcode = '12345';
    const errorMessage = 'An error occurred.';

    spyOn(postcodeService, 'getAddressFromBack').and.returnValue(new Observable((subscriber) => {
      subscriber.error(new Error(errorMessage));
    }));

    component.postcode = postcode;
    component.searchAddress();

    expect(postcodeService.getAddressFromBack).toHaveBeenCalledWith(postcode);
    expect(component.error).toBeTrue();
    expect(component.messageError).toBe(errorMessage);
    expect(component.addresses).toEqual([]);
  });
});