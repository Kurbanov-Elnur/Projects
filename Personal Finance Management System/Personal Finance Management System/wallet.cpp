#include "wallet.h"

wallet::wallet(std::string& _name, std::string& _surname, std::string& _patronomic, uint16_t& _dayOfBirth, uint16_t& _monthOfBirth, uint16_t& _yearOfBirth,
	std::string& _ownerEmail, std::string& _ownerPhone, std::string& _currency, uint16_t& _ID, uint16_t& _securityCode, uint16_t& _balance,
	uint16_t& _transactionLimit, uint16_t& _dailyLimit)
{
	this->ownerData = new personalData(_name, _surname, _patronomic, _dayOfBirth, _monthOfBirth, _yearOfBirth);
	this->ID = new uint16_t{ _ID };
	this->securityCode = new uint16_t{ _securityCode };
	this->ownerEmail = _ownerEmail;
	this->ownerPhone = _ownerPhone;
	this->balance = new uint16_t{ _balance };
	this->currency = _currency;
	this->transactionLimit = new uint16_t{ _transactionLimit };
	this->dailyLimit = new uint16_t{ _dailyLimit };
}

uint16_t wallet::getID() const
{
	return *this->ID;
}

uint16_t wallet::getSecurityCode() const
{
	return *this->securityCode;
}

wallet:: ~wallet()
{
	delete this->ownerData;
	delete this->ID;
	delete this->securityCode;
	delete this->balance;
	delete this->transactionLimit;
	delete this->dailyLimit;
}