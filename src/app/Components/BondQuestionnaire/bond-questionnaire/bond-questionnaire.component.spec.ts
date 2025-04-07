import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BondQuestionnaireComponent } from './bond-questionnaire.component';

describe('BondQuestionnaireComponent', () => {
  let component: BondQuestionnaireComponent;
  let fixture: ComponentFixture<BondQuestionnaireComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BondQuestionnaireComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BondQuestionnaireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
