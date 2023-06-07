import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PostcodeService } from './postcode.service';

describe('PostcodeService', () => {
  let service: PostcodeService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PostcodeService]
    });
    service = TestBed.inject(PostcodeService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should retrieve address from postcodes.io API', (done: DoneFn) => {
    const postcode = '12345';

    const mockResponse = {
      // Mocked response from the API
      // Adjust the properties and values as per your needs
      property1: 'value1',
      property2: 'value2'
    };

    service.getAddress(postcode).subscribe((addressResult) => {
      expect(addressResult).toEqual(mockResponse);
      done();
    });

    const request = httpMock.expectOne(`https://api.postcodes.io/postcodes/${postcode}`);
    expect(request.request.method).toBe('GET');

    request.flush(mockResponse);
  });

  it('should retrieve address from the back-end API', (done: DoneFn) => {
    const postcode = '12345';

    const mockResponse = {
      // Mocked response from the back-end API
      // Adjust the properties and values as per your needs
      property1: 'value1',
      property2: 'value2'
    };

    service.getAddressFromBack(postcode).subscribe((addressResult) => {
      expect(addressResult).toEqual(mockResponse);
      done();
    });

    const request = httpMock.expectOne(`https://localhost:7187/api/Address/${postcode}`);
    expect(request.request.method).toBe('GET');

    request.flush(mockResponse);
  });
});