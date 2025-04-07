import { TestBed } from '@angular/core/testing';

import { CouponeReportService } from './coupone-report.service';

describe('CouponeReportService', () => {
  let service: CouponeReportService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CouponeReportService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
