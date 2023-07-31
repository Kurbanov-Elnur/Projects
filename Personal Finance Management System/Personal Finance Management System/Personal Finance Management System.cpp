#include "wallet.h"

int main()
{
	std::string _name{"ELNUR"};
	std::string _surname{"MAMED"};
	std::string _patronomic{"ASDSA"};
	uint16_t _dayOfBirth{12};
	uint16_t _monthOfBirth{12};
	uint16_t _yearOfBirth{2004};
	std::string _ownerEmail{"asd"};
	std::string _ownerPhone{"sda"};
	std::string _currency{"asd"};
	uint16_t _ID{12};
	uint16_t _securityCode{12};
	uint16_t _balance{12};
	wallet wallet1(_name, _surname, _patronomic, _dayOfBirth, _monthOfBirth, _yearOfBirth, _ownerEmail, _ownerPhone, _currency, _ID, _securityCode, _balance);
	
	wallet1.addCard();

	return 0;
}