#include "card.h"
#pragma once

class wallet
{
private:
	std::string ID{};
	uint16_t* securityCode{};
	Balance* balance{};
public:
	uint16_t* cardsCount = new uint16_t{};
	card** cards = new card*[10];
	personalData* ownerData{};
	std::string ownerEmail{};
	std::string ownerPhone{};
	std::string currency{};
	uint16_t* dailySpendingLimit{};
	Transaction** Transactions = new Transaction*[50]{};
	uint16_t* tranasctionCount = new uint16_t{50};

	wallet(personalData&, std::string&, std::string&, std::string&, std::string&, uint16_t&, Balance&);

	wallet();

	wallet(const wallet& _other);

	friend std::ostream& operator << (std::ostream& os, const wallet _wallet)
	{
		os
			<< *_wallet.ownerData
			<< "Owner email: " << _wallet.ownerEmail << std::endl
			<< "Owner phone: " << _wallet.ownerPhone << std::endl
			<< "Wallet ID: " << _wallet.ID << std::endl
			<< "Wallet security code: " << *_wallet.securityCode << std::endl
			<< "Wallet currency: " << _wallet.currency << std::endl
			<< "Balance: " <<  * _wallet.balance 
			<< "Cards in wallet: " << std::endl;
		for (size_t i = 0; i < *_wallet.cardsCount; i++)
		{
			os << "Card number: " << _wallet.cards[i]->getCardNumber();
		}
		return os;
	}

	friend json& operator << (json& jsonData, const wallet _wallet)
	{
		jsonData << *_wallet.ownerData;
		jsonData["email"] = _wallet.ownerEmail;
		jsonData["phone"] = _wallet.ownerPhone;
		jsonData["ID"] = _wallet.ID;
		jsonData["securityCode"] = *_wallet.securityCode;
		jsonData["currency"] = _wallet.currency;
		jsonData << *_wallet.balance;

		return jsonData;
	}

	friend json& operator>>(json& jsonData, wallet& _wallet)
	{
		jsonData >> *_wallet.ownerData;
		_wallet.ownerEmail = jsonData["email"];
		_wallet.ownerPhone = jsonData["phone"];
		_wallet.ID = jsonData["ID"];
		*_wallet.securityCode = jsonData["securityCode"];
		_wallet.currency = jsonData["currency"];
		jsonData >> *_wallet.balance;
		return jsonData;
	}

	friend std::istream& operator>>(std::istream& is, wallet& _wallet)
	{
		is >> *_wallet.ownerData >> _wallet.ownerEmail >> _wallet.ownerPhone >>  _wallet.ID >> *_wallet.securityCode >> _wallet.currency >> *_wallet.balance;
		return is;
	}

	void addCard();
	void cardReplenishment();
	void walletReplenishment();

	Balance getBalance() const;
	std::string getID() const;
	uint16_t getSecurityCode() const;
};