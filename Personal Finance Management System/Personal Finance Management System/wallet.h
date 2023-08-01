#include "card.h"
#pragma once

class wallet
{
private:
	uint16_t* ID{};
	uint16_t* securityCode{};
	float* balance{};
public:
	uint16_t* cardsCount = new uint16_t{};
	card** cards = new card*[10];
	personalData* ownerData{};
	std::string ownerEmail{};
	std::string ownerPhone{};
	std::string currency{};
	uint16_t* dailySpendingLimit{};

	wallet() = default;

	wallet(personalData&, std::string&, std::string&, std::string&, uint16_t&, uint16_t&, float&);

	void addCard();
	void cardReplenishment();

	uint16_t getBalance() const;
	uint16_t getID() const;
	uint16_t getSecurityCode() const;
};